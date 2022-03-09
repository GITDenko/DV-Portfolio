import React, { useState } from "react";
import GetRequest from "../components/httprequests/GetRequest";
import { Link } from "react-router-dom";

const MainCategory = () => {
  const maincategories = GetRequest("/api/maincategory/")
  
  return (
    <div className="container">
      {(() => {
        try {
          return maincategories.map((maincategory, i) => (
            <Link
              className='Product fade-in-image'
              key={i}
              to="/Subcategory/"
              state={{
                selectedMainCategory: maincategory,
              }}>
                <img className="ProductImg" src={maincategory.imageSrc}/>
                <h2 className="ProductText">{maincategory.name}</h2>
            </Link>
          ))
        } catch(e){}
      })()}
    </div>
  );
}

export default MainCategory;