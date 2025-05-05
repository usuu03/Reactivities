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
import SelectInput from "../../../app/shared/components/SelectInput";
import { categoryOptions } from "./__helper__/categoryOptions";
import DateTimeInput from "../../../app/shared/components/DateTimeInput";
import LocationInput from "../../../app/shared/components/LocationInput";

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
		if (activity)
			reset({
				...activity,
				location: {
					venue: activity.venue,
					city: activity.city,
					latitude: activity.latitude,
					longitude: activity.longitude,
				},
			});
	}, [activity, reset]);

	// Function to handle form submission
	const onSubmit = async (data: ActivitySchema) => {
		const { location, ...rest } = data;
		const activityData = {
			...rest,
			...location,
		};
		try {
			if (activity) {
				updateActivity.mutate(
					{
						...activity,
						...activityData,
					},
					{
						onSuccess: () => navigate(`/activities/${activity.id}`),
					},
				);
			} else {
				createActivity.mutate(
					{
						...activityData,
						id: "",
						isCancelled: false,
						city: activityData.city || "",
					},
					{
						onSuccess: (id) => {
							navigate(`/activities/${id}`);
						},
					},
				);
			}
		} catch (error) {
			console.error(error);
		}
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
				<Box display="flex" gap={3}>
					<SelectInput
						items={categoryOptions}
						label="Category"
						name="category"
						control={control}
					/>
					<DateTimeInput label="Date" control={control} name="date" />
				</Box>

				<LocationInput
					control={control}
					label="Enter the location"
					name="location"
				/>
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
