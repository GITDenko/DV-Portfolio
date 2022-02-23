import React from "react";
import './Products.css';
import GetRequest from "../components/httprequests/GetRequest";
import { useLocation } from "react-router-dom";

const Photo = (url) => {
  const maincategory = useLocation().state.selectedMainCategory;
  const photos = GetRequest(url + "/" + maincategory.id + "/photos");
  console.log(maincategory.id)

  return (
    <>
      {(() => {
        try {
          return photos.map((photo, i) => (
              <div className="Product" key={i}>
                <h2 className="ProductText">{photo.imageUrl}</h2>
              </div>
          ))
        } catch(e){}
      })()}
    </>
  );
}

export default Photo;