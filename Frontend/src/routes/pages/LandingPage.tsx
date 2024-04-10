import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';

export default function LandingPage() {
    const navigate = useNavigate();
    const imageUrl = 'https://restaurantcarousel.se/wp-content/uploads/2023/08/matkassar.png';
    const registerButtonClicked = () =>{
        navigate('/signup');
    }
    const loginButtonClicked = () =>{
        navigate('/login');
    }
  return (
  
   <Box height={'100%'}>
        <Typography textAlign={'center'} gutterBottom variant='h2'  > Exjobb</Typography>
      <div>
        <h1 className='text-red-600'> hello test</h1>
      </div>
    <Card>
    <CardContent>
      <Typography gutterBottom variant="h5" component="div">
        Bakgrund
      </Typography>
      <Typography variant="body2" color="text.secondary">
     Ingen har väl undgått att priserna på mat bara har ökat. Det finns tjänster som kan jämföra priser på mat men ingen som ger dig förslag på vad du faktiskt kan laga för nånting. 

      </Typography>
    </CardContent>
  </Card>
    <Card>
      <CardMedia
      component='img'
       image={imageUrl}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          Om xxxxx
        </Typography>
        <Typography variant="body2" color="text.secondary">
         Vi hämtar data från Ica och Willys och baserat på ditt postnummer hittar vi erbjudanden i de närmsta butikerna. 
        Du får sedan förslag på vad du kan laga baserat på extrapriserna från vår receptdatabas. Databasen fylls regelbundet på av våra användare
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="large" onClick={registerButtonClicked}>Registera dig</Button>
        <Button size="small" onClick={loginButtonClicked}>Redan medlem? Logga in</Button>
      </CardActions>
    </Card>
    </Box>
  );
}