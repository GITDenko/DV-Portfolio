import React from "react";
import GetRequest from "../components/httprequests/GetRequest";

const Website = (props) => {

  const websites = GetRequest(props.url);

  return (
      <>
        {(() => {
          try {
            return websites.map((website, i) => (
                <div className="Product" key={i}>
                  <img className="ProductImg" src={website.imageSrc}/>
                  <h2 className="ProductText">{website.productUrl}</h2>
                </div>
            ))
          } catch(e){}
        })()}
      </>
  );
}

export default Website;