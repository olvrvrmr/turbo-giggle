import React, { useState } from "react";
import { createConfiguration } from "../api/configAPI";
import { Button, Container, TextField, Typography } from "@mui/material";

function AddConfig() {
  const [rowKey, setRowKey] = useState("");
  const [pollingFrequency, setPollingFrequency] = useState("");
  const [downstreamServices, setDownstreamServices] = useState([]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newConfig = {
      rowKey,
      pollingFrequency: parseInt(pollingFrequency, 10),
      downstreamServices: downstreamServices.map((str) => str.trim()),
    };
    JSON.parse(JSON.stringify(newConfig));

    console.log("Payload to be sent:", newConfig);

    try {
      await createConfiguration(newConfig);
      alert("Configuration added successfully!");
      // Redirect to /configurations after a successful submission
      //window.location.href = '/configurations'; // You can use a more robust routing solution here
    } catch (error) {
      alert(`An error occurred: ${error.message}`);
    }
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Add Configuration
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          fullWidth
          label="Row Key"
          variant="outlined"
          margin="normal"
          value={rowKey}
          onChange={(e) => setRowKey(e.target.value)}
        />
        <TextField
          fullWidth
          label="Polling Frequency"
          variant="outlined"
          margin="normal"
          type="number"
          value={pollingFrequency}
          onChange={(e) => setPollingFrequency(e.target.value)}
        />
        <TextField
          fullWidth
          label="Downstream Services"
          variant="outlined"
          margin="normal"
          helperText="Comma-separated list"
          value={downstreamServices.join(", ")} // Join the array for display
          onChange={(e) => setDownstreamServices(e.target.value.split(","))} // Split the input into an array
        />
        <Button variant="contained" color="primary" type="submit">
          Add Configuration
        </Button>
      </form>
    </Container>
  );
}

export default AddConfig;
