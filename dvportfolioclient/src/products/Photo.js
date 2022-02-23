import React from "react";
import './Products.css';
import GetRequest from "../components/httprequests/GetRequest";

const Photo = (props) => {

  const photos = GetRequest(props.url);

  return (
    <>
      {(() => {
        try {
          return photos.map((photo, i) => (
              <div className="Product" key={i}>
                <h2 className="ProductText">{photo.productUrl}</h2>
              </div>
          ))
        } catch(e){}
      })()}
    </>
  );
}

export default Photo;