import React, { useState, useEffect } from "react";
import axios from "axios";
import "../index.css";

const defaultImageSrc = '/img/missingimage.png'
const initialFieldValues = {
  name: '', //imageName
  imageUrl: defaultImageSrc, //imageSrc
  imageFile: null //imageFile
}

const Admin = () => {
  const url = '/api/maincategory'

  const [values, setValues] = useState(initialFieldValues)
  const [errors, setErrors] = useState({})

  const handleInputChange = e => {
    const {name, value} = e.target;
    setValues({
      ...values,
      [name]: value
    })
  }

  const showPreview = e => {
    if(e.target.files && e.target.files[0]){
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = x => {
        setValues({
          ...values,
          imageFile,
          imageUrl: x.target.result
        })
      }
      reader.readAsDataURL(imageFile)
    }
    else{
      setValues({
        ...values,
        imageFile: null,
        imageUrl: '/img/missingimage.png'
      })
    }
  }

  const validate = () => {
    let temp = {}
    temp.name = values.name==""?false:true;
    temp.imageUrl = values.imageUrl==defaultImageSrc?false:true;
    setErrors(temp)
    return Object.values(temp).every(x => x==true)
  }

  const handleFormSubmit = e => {
    e.preventDefault()
    if(validate()){
      const formData = new FormData()
      //formData.append('id', values.id) //Behövs nog ej
      formData.append('name', values.name)
      //formData.append('hidden', values.hidden) // Behövs nog ej
      formData.append('imageUrl', values.imageUrl)
      formData.append('imageFile', values.imageFile)
      addOrEdit(formData, resetForm)
    }
  }

  const applyErrorClass = field => ((field in errors && errors[field]==false)?' invalid-field': '')


  const maincategoryAPI = ( url = '/api/maincategory') => {
    return {
      fetchAll: () => axios.get(url),
      create: newRecord => axios.post(url, newRecord),
      update: (id, updateRecord) => axios.put(url, updateRecord),
      delete: id => axios.delete(url + id)
    }
  }

  const addOrEdit = (formData, onSuccess) => {
    maincategoryAPI().create(formData)
    .then(res => {
      onSuccess();
    })
    .catch(err => console.log(err))
  }

  const resetForm = () =>{
    setValues(initialFieldValues)
    document.getElementById('image-uploader').value = null;
    setErrors({});
  }

  return (
    <div className="container">
     {/* En metod för att lägga till MainCategory */}
        <div className="row">
          <div className="col-md-12 text-center">
              <h1 className="display-4">Maincategories</h1>
            </div>
        </div>
        <div className="col-md-4">
          <div className="container text-center">
            <p className="lead">Maincategory</p>
          </div>
          <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
            <div className="card">
              <img src={values.imageUrl}/>
              <div className="card-body">
                <div className="form-group">
                  <input type="file" accept="image/*" className={"form-control-file"+applyErrorClass('imageUrl')} 
                  onChange={showPreview} id="image-uploader"/>
                </div>
                <div className="form-group">
                  <input className={"form-control"+applyErrorClass('name')} placeholder="Main Category Name" name="name" 
                  value={values.name} 
                  onChange = {handleInputChange} />
                </div>
                <div className="form-group text-center">
                  <button type="submit" className="btn btn-light">Submit</button>
                </div>
              </div>
            </div>
          </form>
        </div>
        
        {/* LIST OF MAINCATEGORIES */}
        <div className="col-md-8">
          List of Maincategories
        </div>

    </div>
  );
}


export default Admin;

