import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { initUsers } from './DataBase.js';
import App from './App';

initUsers();
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
