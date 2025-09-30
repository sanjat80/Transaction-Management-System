import axios from "axios";
import API_CONFIG from '../config';

const BASE_URL = API_CONFIG.BACKEND_URL;
console.log("Backend URL:", BASE_URL);

export const transactionApi = {
  getAllTransactions: async() => {
    const res = await axios.get(BASE_URL);
    return res.data;
  },

  CreateTransaction: async (transaction) =>{
    const res = await axios.post(BASE_URL, transaction);
    return res.data;
  }
};