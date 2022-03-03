import React, { useState } from "react";
import axios from "axios";

const Admin = () => {
  const url = '/api/maincategory'

  const defaultImageSrc = '/img/missingimage.png'

  const [data, setData] = useState(
      {name: '', imageUrl: defaultImageSrc}
    );

  const handleChangeName = (e) => {
    setData({...data, name: e.target.value})
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    axios.post(url, data)
    .then(function (response) {console.log(response);})
    .catch(function (error) {console.log(error);});
  }
  
  const showPreview = e =>{
    if(e.target.files && e.target.files[0]){
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = x =>{
        setData({
          ...data,
          imageFile,
          imageUrl: x.target.result
        })
      }
      reader.readAsDataURL(imageFile)
    }
    //Kanske inte behöver denna
    else{
      setData({
        ...data,
        imageFile: null,
        imageUrl: defaultImageSrc
      })
    }
  }

  return (
    <>
     {/* En metod för att lägga till MainCategory */}
     <h2>Create a Main Category</h2>
     <img src={data.imageUrl}/>
     <form onSubmit={handleSubmit}>
      
      <p>Name on main category:</p>
      <input type='text' name="Name" value={data.name} onChange={(e) => handleChangeName(e)} required/>
      
      <p>Filename:</p>
      <input type='file' accept="image/" onChange={showPreview}/>

      <button type='submit'> Create </button>
     </form>

      {/* LIST OF MAINCATEGORIES */}
      <div>
        
      </div>
    </>
  );
}

export default Admin;

