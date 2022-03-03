import React, { useState } from "react";
import axios from "axios";

const Admin = () => {
  const url = '/api/maincategory'

  const [data, setData] = useState(
      {name: '', imageUrl: ''}
    );

  const handleChangeFilename = (event) => {
    setData({...data, imageUrl: event.target.value})
  }

  const handleChangeName = (e) => {
    setData({...data, name: e.target.value})
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    axios.post(url, data)
    .then(function (response) {console.log(response);})
    .catch(function (error) {console.log(error);});
  }

  return (
    <>
     {/* En metod för att lägga till MainCategory */}
     Create a Main Category
     <form onSubmit={handleSubmit}>
      Name on main category:
      <input type='text' name="Name" value={data.name} onChange={(e) => handleChangeName(e)} required/>
      Filename:
      <input type='text' onChange={(e) => handleChangeFilename(e)} value={data.imageUrl} required/>
      <button type='submit'> Create </button>
     </form>

     {/* En metod för att lägga till Subcategory */}
     {/* En metod för att lägga till Photo */}
     {/* En metod för att lägga till Video */}
     {/* En metod för att lägga till Website */}
    </>
  );
}

export default Admin;

