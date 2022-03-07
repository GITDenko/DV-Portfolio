import React from "react";
import GetRequest from "../components/httprequests/GetRequest";
import { Link, useLocation } from "react-router-dom";
import Products from "../products/Products";

const Subcategory = () => {
  const maincategory = useLocation().state.selectedMainCategory;
  const url = "/api/maincategory/" + maincategory.id + "/subcategory";

  const subcategories = GetRequest(url);

  return (
    <>
      {(() => {
        try {
          return subcategories.map((subcategory, i) => (
            <Link 
              key={i}
              to="/Products/"
              state={{
                selectedSubcategory: subcategory,
                selectedUrl: url
              }}>
              <div className="Product">
                <h2 className="ProductText">{subcategory.name}</h2>
              </div>
            </Link>
          ))
        } catch(e){}
      })()}
    </>
  );
}

export default Subcategory;