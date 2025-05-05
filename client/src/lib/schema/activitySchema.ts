import { z } from "zod";

// A helper function to create a required string schema with a custom error message
const requiredString = (fieldName: string) =>
	z
		.string({ required_error: `${fieldName} is required` })
		.min(3, { message: `${fieldName} is required` });

// Defining a schema for an activity object
export const activitySchema = z.object({
	title: requiredString("Title"),
	description: requiredString("Description"),
	category: requiredString("Category"),
	date: requiredString("Date"),
	city: requiredString("City"),
	venue: requiredString("Venue"),
});

// Exporting the schema as a type
export type ActivitySchema = z.infer<typeof activitySchema>;
