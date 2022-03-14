import React from "react";
import GetRequest from "../components/httprequests/GetRequest";

const Photo = (props) => {

  const photos = GetRequest(props.url);

  return (
    <>
      {(() => {
        try {
          return photos.map((photo, i) => (
              <div className="Product" key={i}>
                <img className="ProductImg" src={photo.imageSrc}/>
              </div>
          ))
        } catch(e){}
      })()}
    </>
  );
}

export default Photo;