// src/api/configAPI.js
import axios from 'axios';

const baseURL = process.env.REACT_APP_CONFIG_API_URL;

export const fetchConfigurations = async () => {
  try {
    const { data } = await axios.get(`${baseURL}/configurations`);
    return data;
  } catch (error) {
    console.error("Error fetching configurations:", error);
  }
};

export const createConfiguration = async (config) => {
  try {
    const { data } = await axios.post(`${baseURL}/configurations`, config);
    return data;
  } catch (error) {
    console.error("Error creating configuration:", error);
  }
};
