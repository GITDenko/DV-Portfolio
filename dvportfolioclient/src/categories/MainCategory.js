import React from "react";
import '../products/Products.css';
import GetRequest from "../components/httprequests/GetRequest";
import { Link } from "react-router-dom";

const MainCategory = () => {

  ///https://localhost:44388/api/maincategory/1/subcategory
  const maincategories = GetRequest("/api/maincategory/")
  console.log(maincategories)

  return (
    <>
      {(() => {
        try {
          return maincategories.map((maincategory, i) => (
            <Link 
              key={i}
              to="/Subcategory/"
              state={{
                selectedMainCategory: maincategory,
              }}>
              <div className="Product">
                <h2 className="ProductText">{maincategory.name}</h2>
              </div>
            </Link>
          ))
        } catch(e){}
      })()}
    </>
  );
}

export default MainCategory;