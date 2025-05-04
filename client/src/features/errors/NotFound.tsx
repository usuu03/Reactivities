import { SearchOff } from "@mui/icons-material";
import { Paper, Typography, Button } from "@mui/material";
import { Link } from "react-router";

export default function NotFound() {
	return (
		<Paper
			sx={{
				height: 400,
				display: "flex",
				flexDirection: "column",
				justifyContent: "center",
				alignItems: "center",
				p: 6,
			}}
		>
			<SearchOff />
			<Typography gutterBottom variant="h3">
				Oops - We could not find what you were looking for
			</Typography>
			<Button fullWidth component={Link} to="/activities">
				Return to activities page
			</Button>
		</Paper>
	);
}
