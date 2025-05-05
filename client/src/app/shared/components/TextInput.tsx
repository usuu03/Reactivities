import { TextField, type TextFieldProps } from "@mui/material";
import {
	useController,
	type FieldValues,
	type UseControllerProps,
} from "react-hook-form";

// Defining the props for the TextInput component
// It combines Material-UI's TextFieldProps and React Hook Form's UseControllerProps
type TextInputProps<T extends FieldValues> = {} & TextFieldProps &
	UseControllerProps<T>;

// A reusable TextInput component that integrates Material-UI's TextField with React Hook Form
export default function TextInput<T extends FieldValues>(
	props: TextInputProps<T>,
) {
	const { field, fieldState } = useController({ ...props });
	return (
		<TextField
			{...props}
			{...field}
			value={field.value || ""}
			fullWidth
			variant="outlined"
			error={!!fieldState.error}
			helperText={fieldState.error?.message}
		/>
	);
}
