import { Box, Button, Paper, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useNavigate, useParams } from "react-router";
import { useForm } from "react-hook-form";
import { useEffect } from "react";
import {
	activitySchema,
	type ActivitySchema,
} from "../../../lib/schema/activitySchema";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../../app/shared/components/TextInput";

export default function ActivityForm() {
	// Initializing the form with React Hook Form and Zod resolver
	const { control, reset, handleSubmit } = useForm<ActivitySchema>({
		mode: "onTouched",
		resolver: zodResolver(activitySchema), // Using Zod schema for validation
	});
	const { id } = useParams();
	// Custom hook to fetch activity data and provide update/create functionality
	const { updateActivity, createActivity, activity, isLoadingActivty } =
		useActivities(id);
	const navigate = useNavigate();

	// Effect to reset the form with activity data when it changes
	useEffect(() => {
		if (activity) reset(activity);
	}, [activity, reset]);

	// Function to handle form submission
	const onSubmit = async (data: ActivitySchema) => {
		console.log(data);
	};

	// Show a loading message while the activity data is being fetched
	if (isLoadingActivty) return <Typography>Loading activity...</Typography>;

	return (
		<Paper sx={{ padding: 3, borderRadius: 3 }}>
			<Typography variant="h5" gutterBottom color="primary">
				{activity ? "Edit Activity" : "Create Activity"}
			</Typography>
			<Box
				component="form"
				onSubmit={handleSubmit(onSubmit)}
				display="flex"
				flexDirection="column"
				gap={3}
			>
				<TextInput label="Title" control={control} name="title" />
				<TextInput
					label="Description"
					control={control}
					name="description"
					multiline
					rows={3}
				/>
				<TextInput label="Category" control={control} name="category" />
				<TextInput label="Date" control={control} name="date" />
				<TextInput label="City" control={control} name="city" />
				<TextInput label="Venue" control={control} name="venue" />
				<Box display="flex" justifyContent="end" gap={3}>
					<Button
						color="inherit"
						onClick={() => navigate(`/activities/${activity?.id}`)}
					>
						Cancel
					</Button>
					<Button
						type="submit"
						variant="contained"
						color="primary"
						disabled={updateActivity.isPending || createActivity.isPending}
					>
						Submit
					</Button>
				</Box>
			</Box>
		</Paper>
	);
}
