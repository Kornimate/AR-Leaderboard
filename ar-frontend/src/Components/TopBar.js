import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';

const TopBar = () => {
    return (
    <Box sx={{ width: "100%", flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar sx={{ bgcolor: "#E74C3C"}}>
          <Typography variant="h4" component="div" sx={{ flexGrow: 0, textAlign: "left" }}>
          PicARsso
          </Typography>
        </Toolbar>
      </AppBar>
    </Box>
    )
}

export default TopBar;