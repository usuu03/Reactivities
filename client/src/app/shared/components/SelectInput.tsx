import {
	FormControl,
	FormHelperText,
	InputLabel,
	MenuItem,
	Select,
} from "@mui/material";
import type { SelectProps } from "@mui/material/Select";
import {
	useController,
	type FieldValues,
	type UseControllerProps,
} from "react-hook-form";

// Defining the props for the TextInput component
// It combines Material-UI's TextFieldProps and React Hook Form's UseControllerProps
type Props<T extends FieldValues> = {
	items: { text: string; value: string }[];
	label: string;
} & UseControllerProps<T> &
	Partial<SelectProps>;

// A reusable SelectInput component that integrates Material-UI's TextField with React Hook Form
export default function SelectInput<T extends FieldValues>(props: Props<T>) {
	const { field, fieldState } = useController({ ...props });
	return (
		<FormControl fullWidth error={!!fieldState.error}>
			<InputLabel>{props.label}</InputLabel>
			<Select
				value={field.value || ""}
				label={props.label}
				onChange={field.onChange}
			>
				{props.items.map((item) => (
					<MenuItem key={item.value} value={item.value}>
						{item.text}
					</MenuItem>
				))}
			</Select>
			<FormHelperText>{fieldState.error?.message}</FormHelperText>
		</FormControl>
	);
}
