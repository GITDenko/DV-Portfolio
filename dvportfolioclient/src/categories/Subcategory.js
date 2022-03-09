import React from "react";
import GetRequest from "../components/httprequests/GetRequest";
import { Link, useLocation } from "react-router-dom";
import Products from "../products/Products";

const Subcategory = () => {
  const maincategory = useLocation().state.selectedMainCategory;
  const url = "/api/maincategory/" + maincategory.id + "/subcategory";

  const subcategories = GetRequest(url);
  console.log(subcategories)

  return (
    <div className="container">
      {(() => {
        try {
          return subcategories.map((subcategory, i) => (
            <Link 
              className="Product fade-in-image"
              key={i}
              to="/Products/"
              state={{
                selectedSubcategory: subcategory,
                selectedUrl: url
              }}>
                <img className="ProductImg" src={subcategory.imageSrc}/>
                <h2 className="ProductText">{subcategory.name}</h2>
            </Link>
          ))
        } catch(e){}
      })()}
    </div>
  );
}

export default Subcategory;