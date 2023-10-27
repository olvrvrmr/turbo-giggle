import React, { useEffect, useState } from "react";
import { fetchConfigurations } from "../api/configAPI";
import {
  Container,
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import Chip from "@mui/material/Chip";
import { Link } from "react-router-dom";
import { CircularProgress } from "@mui/material";

function ConfigList() {
  const [configs, setConfigs] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await fetchConfigurations();
        setConfigs(data);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Configurations
      </Typography>
      <Link to="/configurations/add" style={{ textDecoration: 'none' }}>
        <Button variant="contained" color="primary" style={{ marginBottom: '20px' }}>
          Add New Configuration
        </Button>
      </Link>
      {loading ? (
        <CircularProgress />
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Row Key</TableCell>
                <TableCell>Polling Frequency</TableCell>
                <TableCell>Downstream Services</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {configs.map((config, index) => (
                <TableRow key={index}>
                  <TableCell>
                    <Link to={`/configurations/${config.rowKey}`}>
                      {config.rowKey}
                    </Link>
                  </TableCell>
                  <TableCell>{config.pollingFrequency}</TableCell>
                  <TableCell>
                    {config.downstreamServices.map((service, i) => (
                      <Chip label={service.trim()} key={i} />
                    ))}
                  </TableCell>
                  <Link to={`/configurations/edit/${config.rowKey}`}>Edit</Link>{" "}
                  |
                  <a
                    href="#"
                    onClick={() => {
                      /* delete function here */
                    }}
                  >
                    Delete
                  </a>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
}

export default ConfigList;
