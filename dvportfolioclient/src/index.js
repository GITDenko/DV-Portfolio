import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Header from './components/Header';
import MainCategory from './categories/MainCategory';
import Subcategory from './categories/Subcategory';
import Products from './products/Products';
import Admin from './admin/Admin';
import 'bootstrap/dist/css/bootstrap.min.css';

ReactDOM.render(
  <React.StrictMode>
    <BrowserRouter>
      <Header/>
      <Routes>
          <Route exact path="/home" element={<MainCategory />} />
          <Route exact path="/*" element={<MainCategory />} />
          <Route exact path="/subcategory" element={<Subcategory />} />
          <Route exact path="/products" element={<Products />} />
          <Route exact path="/admin" element={<Admin />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
