import { Typography , Grid, CssBaseline, Paper, Avatar, TextField, FormControlLabel, Checkbox, Button, Box } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";


export default function LogInPage(){

  return (
    <Grid container component="main" >
      <CssBaseline />
      <Grid item xs={false} sm={4} md={7} />
      <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
        <div >
          <Avatar >
           
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <form noValidate>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              autoFocus
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
    
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item xs>
                <Link to={"apa"}>
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link to={"apa"}>
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
            <Box mt={5}>
             
            </Box>
          </form>
        </div>
      </Grid>
    </Grid>
  );
}


