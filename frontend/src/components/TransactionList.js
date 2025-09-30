import React, { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Button, Dialog, DialogContent, Box, Snackbar, Alert } from '@mui/material';
import TransactionForm from './TransactionForm';
import { transactionApi } from '../api/transactionApi';

const statusColors = {
  Failed: '#e57373',   
  Pending: '#fff176',  
  Settled: '#81c784',  
};
const columns = [
  { field: 'transactionDate', headerName: 'Transaction date', width: 180 },
  { field: 'accountNumber', headerName: 'Account number', width: 180 },
  { field: 'accountHolderName', headerName: 'Account holder name', width: 200 },
  { field: 'amount', headerName: 'Amount', width: 120 },
  {
    field: 'status',
    headerName: 'Status',
    width: 130,
    renderCell: (params) => (
      <Box
        sx={{
          backgroundColor: statusColors[params.value] || '#eee',
          color: params.value === 'Pending' ? '#000' : '#fff',
          px: 2,
          py: 0.5,
          borderRadius: 2,
          fontWeight: 'bold',
          textAlign: 'center',
          minWidth: 80,
        }}
      >
        {params.value}
      </Box>
    ),
  },
];

export default function TransactionTable() {
  const [rows, setRows] = useState([]);
  const [open, setOpen] = useState(false);
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });
  const [loading, setLoading] = useState(false);
  const [backendErrors, setBackendErrors] = useState({});

  const fetchTransactions = async () => {
    try {
      const data = await transactionApi.getAllTransactions();
      const rows = data.map((tr, idx) => ({ id: idx + 1, ...tr }));
      setRows(rows);
    } catch {
      setRows([]);
    }
  };

  useEffect(() => {
    fetchTransactions();
  }, []);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const handleFormSubmit = async (form, resetForm) => {
    setLoading(true);
    setBackendErrors({});
    try {
      const payload = {
        ...form,
        amount: parseFloat(form.amount),
        transactionDate: new Date(form.transactionDate).toISOString(),
      };
      await transactionApi.CreateTransaction(payload);
      setSnackbar({ open: true, message: 'Transaction successfully added!', severity: 'success' });
      fetchTransactions();
      resetForm();
      handleClose();
    } catch (err) {
      if (
        err.response &&
        err.response.status === 400 &&
        err.response.data &&
        err.response.data.errors
      ){
        const flatErrors = {};
        Object.entries(err.response.data.errors).forEach(([k, v]) => {
        const key = k.charAt(0).toLowerCase() + k.slice(1);
        flatErrors[key] = v[0]; 
      });
      setBackendErrors(flatErrors);
      }
      else{
        setSnackbar({ open: true, message: 'Error while trying to add new transaction.', severity: 'error' });
      }
    }
      
    setLoading(false);
  };

  return (
    <Box sx={{ maxWidth: 900,    
    width: "100%",
    mx: "auto", 
    mt: 4,
    background: "#fffbe6", 
    borderRadius: 3,
    boxShadow: 2,
    p: 4}}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 2 , width:'100%', maxWidth:900}}>
        <h2 style={{ color: "#070a05ff", fontWeight: "bold", letterSpacing: "2px" }}>
          TRANSACTIONS
        </h2>        
        <Button variant="contained"
        onClick={handleOpen}
        sx={{
          backgroundColor: "#e5ebe2ff",
          color: "#2c2a2aff",
          width: "150px", 
          fontWeight: "bold",
          "&:hover": {
          backgroundColor: "#151814ff", 
        },
        borderRadius: 2,
        textTransform: "none",
        boxShadow: 2,
        height:"50px"
        }}>
          Add transaction
        </Button>
      </Box>
      <DataGrid
        rows={rows}
        columns={columns}
        pageSize={8}
        rowsPerPageOptions={[8]}
        sx={{
          background: '#fff',
          borderRadius: 2,
          boxShadow: 3,
          fontSize: 16,
          '& .MuiDataGrid-columnHeaders': {
            fontWeight: 'bold',
            fontSize: 18
          }
        }}
      />
      <Dialog open={open} onClose={handleClose}>
        <DialogContent>
          <TransactionForm
            onSubmit={handleFormSubmit}
            onCancel={handleClose}
            loading={loading}
            externalErrors={backendErrors}
          />
        </DialogContent>
      </Dialog>
      <Snackbar
        open={snackbar.open}
        autoHideDuration={3000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert severity={snackbar.severity}>{snackbar.message}</Alert>
      </Snackbar>
    </Box>
  );
}