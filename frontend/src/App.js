import React from "react";
import ConfigList from "./components/ConfigList";
import CssBaseline from "@mui/material/CssBaseline";
import { AppBar, Toolbar, Typography, Button } from "@mui/material";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import AddConfig from "./components/AddConfig";

function App() {
  return (
    <>
      <CssBaseline />
      <Router>
        <AppBar position="static">
          <Toolbar>
            <Typography variant="h6" style={{ flexGrow: 1 }}>
              Configuration Management
            </Typography>
            <Link to="/" style={{ textDecoration: "none", color: "inherit" }}>
              <Button color="inherit">Home</Button>
            </Link>
            <Link to="/configurations" style={{ textDecoration: "none", color: "inherit" }}>
              <Button color="inherit">Configurations</Button>
            </Link>
            <Link
              to="/about"
              style={{ textDecoration: "none", color: "inherit" }}
            >
              <Button color="inherit">About</Button>
            </Link>
          </Toolbar>
        </AppBar>
        <Routes>
          <Route path="/"></Route>
          <Route path="/about">{/* Your About component */}</Route>
          <Route path="/configurations" element={<ConfigList />} />
          <Route path="/configurations/add" element={<AddConfig />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
