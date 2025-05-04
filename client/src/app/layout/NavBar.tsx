import {
	AppBar,
	Box,
	Toolbar,
	Typography,
	Container,
	MenuItem,
} from "@mui/material";

import { Group } from "@mui/icons-material";
import { NavLink } from "react-router";
import MenutItemLink from "../shared/components/MenutItemLink";

function NavBar() {
	return (
		<Box sx={{ flexGrow: 1 }}>
			<AppBar
				position="static"
				sx={{
					backgroundImage:
						"linear-gradient(135deg, #182a73 0%, #218aae 69%, #20a7ac 89%)",
				}}
			>
				<Container maxWidth="xl">
					<Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
						<Box>
							<MenuItem
								component={NavLink}
								to="/"
								sx={{ display: "flex", gap: 2 }}
							>
								<Group fontSize="large" />
								<Typography variant="h4" fontWeight="bold">
									Reactivities
								</Typography>
							</MenuItem>
						</Box>
						<Box sx={{ display: "flex", gap: 2 }}>
							<MenutItemLink to="/activities">Activities</MenutItemLink>
							<MenutItemLink to="/createActivity">
								Create Activity
							</MenutItemLink>

							<MenutItemLink to="/errors">Errors</MenutItemLink>
						</Box>
						<MenuItem>User Menu</MenuItem>
					</Toolbar>
				</Container>
			</AppBar>
		</Box>
	);
}

export default NavBar;
