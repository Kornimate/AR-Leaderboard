import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import logo from "../assets/game-jam-logo.png";
import textLogo from "../assets/picarsso-logo.png";
import "../Styles/TopBar.css";

const TopBar = () => {

    function NavigateToItchIo(){
      window.location = "https://platak1sm.itch.io/aar25t2";
    }

    return (
    <Box sx={{ width: "100%", flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar sx={{ bgcolor: "#E74C3C"}}>
        <IconButton
            size="large"
            aria-label="game-jam-logo"
            onClick={NavigateToItchIo}
            color="inherit"
            sx={{ bgcolor: "#9B59B6"}}
          >
            <img src={logo} alt="game-jam-logo" className="gameJamLogo" />
          </IconButton>
          <img src={textLogo} alt="picarsso-logo" className="picarssoLogo" />
          {/* <Typography variant="h4" component="div" sx={{ flexGrow: 0, textAlign: "left" }}>
          PicARsso
          </Typography> */}
        </Toolbar>
      </AppBar>
    </Box>
    )
}

export default TopBar;