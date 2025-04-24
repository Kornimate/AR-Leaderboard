import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';
import { Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import axios from "axios";
import * as signalR from "@microsoft/signalr"
import CONFIGURATION from '../Config/config';

const LeaderBoard = () => {

    const SIGNALR_MESSAGE = "ReceiveUpdate";

    const [list, setList] = useState([]);

    useEffect(() => {
        UpdateList();
    },[])

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
          .withUrl(`${CONFIGURATION.BASE_URL}${CONFIGURATION.SIGNALR_ENDPOINT}`, {
            withCredentials: true
          })
          .withAutomaticReconnect()
          .build();
    
        connection
          .start()
          .then(() => {
            console.log("SignalR Connected");
    
            connection.on(SIGNALR_MESSAGE, (user, message) => {
              UpdateList();
            });
          })
          .catch((err) => console.error("SignalR Connection Error:", err));
    
        return () => {
          connection.stop().then(() => console.log("SignalR Disconnected"));
        };
      }, []);

    function UpdateList(){
        axios.get(`${CONFIGURATION.BASE_URL}${CONFIGURATION.API_ENDPOINT}/list`)
        .then(response => {
            setList(response.data)
        }).catch(error => {
            console.log(error)
        })
    }

    const icon = (rank) => {
        if(rank === 1){
            return <EmojiEventsIcon sx={{color: "#FFD700"}} />
        } else if(rank === 2){
            return <EmojiEventsIcon sx={{color: "#C0C0C0"}} />
        } else if(rank === 3){
            return <EmojiEventsIcon sx={{color: "#CD7F32"}} />
        } else {
            return <span>{rank}</span>
        }
    }

    return (
        <>
            <Typography variant='h1' gutterBottom sx={{marginTop: 2, padding: 2, borderRadius: 4, bgcolor: "#E74C3C"}}>PicARsso Leaderboard</Typography>
            {
                list.length > 0
                ? <List sx={{ width: '100%', maxWidth: 300, bgcolor: "#9B59B6", borderRadius: 3 }}>
                    {
                        list.map((item) => (
                            <ListItem key={item.key}>
                                <ListItemAvatar>
                                    <Avatar sx={{bgcolor: "#2ECC71"}}>
                                        { icon(item.rank) }
                                    </Avatar>
                                </ListItemAvatar>
                                <ListItemText primary={item.name} secondary={`${item.score} ${item.score > 1 ? "points" : "point"}`} />
                            </ListItem>
                        ))
                    }
                    </List>
                : <Typography sx={{fontStyle: "italic"}}>No records yet</Typography>
            }
        </>
    )
}

export default LeaderBoard;