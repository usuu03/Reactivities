import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import agent from "../api/agent";
import type { Activity } from "../types";

export const useActivities = (id?: string) => {
	const queryClient = useQueryClient();
	const { data: activities, isPending } = useQuery({
		queryKey: ["activities"],
		queryFn: async () => {
			const response = await agent.get<Activity[]>("/activities");
			return response.data;
		},
	});

	const { data: activity, isLoading: isLoadingActivty } = useQuery({
		queryKey: ["activities", id],
		queryFn: async () => {
			const response = await agent.get<Activity>(`/activities/${id}`);
			return response.data;
		},
		enabled: !!id,
	});

	const updateActivity = useMutation({
		mutationFn: async (activity: Activity) => {
			await agent.put("/activities/", activity);
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: ["activities"] });
		},
	});

	const createActivity = useMutation({
		mutationFn: async (activity: Activity) => {
			const response = await agent.post("/activities", activity);
			return response.data;
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: ["activities"] });
		},
	});

	const deleteActivity = useMutation({
		mutationFn: async (id: string) => {
			await agent.delete(`/activities/${id}`);
		},
		onSuccess: async () => {
			await queryClient.invalidateQueries({ queryKey: ["activities"] });
		},
	});

	return {
		activities,
		isPending,
		updateActivity,
		createActivity,
		deleteActivity,
		activity,
		isLoadingActivty,
	};
};
