import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Header from './components/Header';
import MainCategory from './categories/MainCategory';
import Subcategory from './categories/Subcategory';
import Products from './products/Products';
import AdminMainCateogry from './admin/AdminMainCateogry';
import AdminSubcategory from './admin/AdminSubcategory';
import AdminPhoto from './admin/AdminPhotos';
import AdminVideo from './admin/AdminVideos';
import AdminWebsite from './admin/AdminWebsites';
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
          <Route exact path="/adminmaincategory" element={<AdminMainCateogry/>} />
          <Route exact path="/adminsubcategory" element={<AdminSubcategory/>} />
          <Route exact path="/adminphotos" element={<AdminPhoto/>} />
          <Route exact path="/adminvideos" element={<AdminVideo/>} />
          <Route exact path="/adminwebsites" element={<AdminWebsite/>} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
