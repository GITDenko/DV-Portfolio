import React from "react";
import Photo from "./Photo";
import Video from "./Video";
import Website from "./Website";
import { Link, useLocation } from "react-router-dom";

const Products = () => {
  
  //const maincategory = useLocation().state.selectedMainCategory; /// Behövsn og inte
  // url behöver hämtas och göras om för alla olika bitarna (photo, video, website)
  const subcategory = useLocation().state.selectedSubcategory;
  const url = useLocation().state.selectedUrl;
  
  const photoURL = (url + "/" + subcategory.id + "/photos")
  const videosURL = (url + "/" + subcategory.id + "/videos")
  const websitesURL = (url + "/" + subcategory.id + "/websites")

  return (
    <>
        <Photo url={photoURL} />
        <Video url={videosURL}/>
        <Website url={websitesURL}/>
    </>
  );
}

export default Products;