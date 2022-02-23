import React from "react";
import './Products.css';
import Photo from "./Photo";
import Video from "./Video";
import Website from "./Website";
import { Link, useLocation } from "react-router-dom";

const Products = () => {
  
  const maincategory = useLocation().state.selectedMainCategory; /// Behövsn og inte
  // url behöver hämtas och göras om för alla olika bitarna (photo, video, website)
  const subcategory = useLocation().state.selectedSubcategory;
  
  return (
    <>
        <Photo/>
        <Video/>
        <Website/>
    </>
  );
}

export default Products;