import React, { useState, useEffect } from 'react';
import { Box, Button, TextField, Typography } from '@mui/material';

const emptyForm = {
  transactionDate: '',
  accountNumber: '',
  accountHolderName: '',
  amount: '',
};

export default function TransactionForm({ onSubmit, onCancel, loading = false, externalErrors = {} }) {
  const [form, setForm] = useState(emptyForm);
  const [errors, setErrors] = useState({});

   useEffect(() => {
    if (externalErrors) setErrors((prev) => ({ ...prev, ...externalErrors }));
  }, [externalErrors]);

  const validate = () => {
    let temp = {};
    temp.transactionDate = form.transactionDate ? '' : 'Transaction date is required field.';
    temp.accountNumber = form.accountNumber ? '' : 'Account number is required field.';
    temp.accountHolderName = form.accountHolderName ? '' : 'Account holder name is required field.';
    temp.amount = form.amount && !isNaN(form.amount) ? '' : 'Amount is required field.';
    setErrors({ ...temp });
    return Object.values(temp).every(x => x === '');
  };

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = e => {
    e.preventDefault();
    if (validate()) {
      onSubmit(form, resetForm);
    }
  };

  const resetForm = () => {
    setForm(emptyForm);
    setErrors({});
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit}
      sx={{
        display: 'flex',
        flexDirection: 'column',
        gap: 3,
        width: 350,
        padding: 3,
        boxShadow: 3,
        background: '#fff',
        margin: '0 auto'
      }}
    >
      <Typography variant="h5" align="center" mb={1}>
        New transaction
      </Typography>
      <TextField
        label="Transaction date"
        type="datetime-local"
        name="transactionDate"
        value={form.transactionDate}
        onChange={handleChange}
        error={!!errors.transactionDate}
        helperText={errors.transactionDate}
        required
      />
      <TextField
        label="Account number"
        name="accountNumber"
        value={form.accountNumber}
        onChange={handleChange}
        error={!!errors.accountNumber}
        helperText={errors.accountNumber}
        required
      />
      <TextField
        label="Account holder name"
        name="accountHolderName"
        value={form.accountHolderName}
        onChange={handleChange}
        error={!!errors.accountHolderName}
        helperText={errors.accountHolderName}
        required
      />
      <TextField
        label="Amount"
        name="amount"
        value={form.amount}
        onChange={handleChange}
        type="number"
        error={!!errors.amount}
        helperText={errors.amount}
        required
      />
      <Box sx={{ display: "flex", justifyContent: "space-between", mt: 2 }}>
        <Button onClick={() => { onCancel && onCancel(); resetForm(); }} color="warning" variant="outlined">
          Cancel
        </Button>
        <Button type="submit" variant="contained" disabled={loading} color="success">
          Save
        </Button>
      </Box>
    </Box>
  );
}