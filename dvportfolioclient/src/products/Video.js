import React from "react";
import './Products.css';
import GetRequest from "../components/httprequests/GetRequest";

const Video = (props) => {
  
  const videos = GetRequest(props.url);

  return (
    <>
      {(() => {
        try {
          return videos.map((video, i) => (
            <iframe width="420" height="315" key={i}
                src={video.productUrl}>
            </iframe> 
          ))
        } catch(e){}
      })()}
    </>
  );
}

export default Video;