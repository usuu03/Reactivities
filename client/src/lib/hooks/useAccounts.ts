import { useMutation } from "@tanstack/react-query";
import type { LoginSchema } from "../schema/loginSchema";
import agent from "../api/agent";

export const useAccount = () => {
	const loginUser = useMutation({
		mutationFn: async (creds: LoginSchema) => {
			await agent.post("/login?useCookies=true", creds);
		},
	});

	return {
		loginUser,
	};
};
