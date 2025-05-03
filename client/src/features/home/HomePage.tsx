import { Group } from "@mui/icons-material";
import { Box, Button, Link, Paper, Typography } from "@mui/material";


export default function HomePage() {
  return (
    <Paper
      sx={{
        color: 'white',
        display: 'flex',
        flexDirection: 'column',
        gap: 6,
        alignItems: 'center',
        justifyContent: 'center',
        height: '100vh',
        backgroundImage: "linear-gradient(135deg, #182a73 0%, #218aae 69%, #20a7ac 89%)"
      }}>
      <Box sx={{ display: 'flex', flexDirection: 'center', alignContent: 'center', color: 'white', gap: 3 }}>
        <Group sx={{ height: 110, width: 110 }} />
        <Typography variant="h1" >
          Reactivities
        </Typography>
      </Box>
      <Typography variant="h1" >
        Welcome to reactivities
      </Typography>
      <Button
        component={Link}
        href='/activities'
        size="large"
        variant="contained"
        sx={{ height: 80, borderRadius: 4, fontSize: '1.5rem' }}
      >
        Take me to the activities!
      </Button>
    </Paper>
  )
}
