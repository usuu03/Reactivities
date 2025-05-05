import {
	type FieldValues,
	useController,
	type UseControllerProps,
} from "react-hook-form";
import { DateTimePicker, type DateTimePickerProps } from "@mui/x-date-pickers";

type Props<T extends FieldValues> = {} & UseControllerProps<T> &
	DateTimePickerProps;

export default function DateTimeInput<T extends FieldValues>(props: Props<T>) {
	const { field, fieldState } = useController({ ...props });
	return (
		<DateTimePicker
			{...props}
			value={field.value ? new Date(field.value) : null}
			onChange={(value) => {
				// biome-ignore lint/style/noNonNullAssertion: <explanation>
				field.onChange(new Date(value!));
			}}
			sx={{ width: "100%" }}
			slotProps={{
				textField: {
					onBlur: field.onBlur,
					error: !!fieldState.error,
					helperText: fieldState.error?.message,
				},
			}}
		/>
	);
}
