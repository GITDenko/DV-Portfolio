import React, { useState, useEffect } from "react";
import axios from "axios";
import "../index.css";
import { BsFillTrashFill } from "react-icons/bs";

const defaultImageSrc = '/img/missingimage.png'
const initialFieldValues = {
  maincategoryId: 0,
  subcategoryId: 0,
  imageUrl: '', //imageName
  imageSrc: defaultImageSrc,
  imageFile: null //imageFile
}

const AdminPhotos = () => {
  const [values, setValues] = useState(initialFieldValues)
  const [errors, setErrors] = useState({})
  const [recordForEdit, setRecordForEdit] = useState(null)
  const [maincategoryList, setMaincategorylist] = useState([])
  const [subcategoryList, setSubcategorylist] = useState([])
  const [photoList, setPhotolist] = useState([])


  //Use effect på maincategory, sub, photo, video etc.
  useEffect(() =>{
    if(recordForEdit != null)
      setValues(recordForEdit);
      refreshMainCategoryList();
    }, [recordForEdit])

  const showPreview = e => {
    if(e.target.files && e.target.files[0]){
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = x => {
        setValues({
          ...values,
          imageFile: null,
          imageSrc: x.target.result
        })
      }
      reader.readAsDataURL(imageFile)
    }
    else{
      setValues({
        ...values,
        imageFile: null,
        imageSrc: defaultImageSrc
      })
    }
  }

  const validate = () => {
    let temp = {}
    temp.imageSrc = values.imageSrc==defaultImageSrc?false:true;
    setErrors(temp)
    return Object.values(temp).every(x => x==true)
  }

  const handleFormSubmit = e => {
    e.preventDefault()
    if(validate()){
      const formData = new FormData()
      formData.append('id', values.id) //Behövs nog ej
      formData.append('maincategoryId', values.maincategoryId)
      formData.append('subcategoryId', values.subcategoryId)
      //formData.append('hidden', values.hidden) // Behövs nog ej
      formData.append('imageUrl', values.imageUrl) //ImageName
      formData.append('imageFile', values.imageFile)
      addOrEdit(formData, resetForm)
    }
  }

  const applyErrorClass = field => ((field in errors && errors[field]==false)?' invalid-field': '')

  //: API FUNCTIONS
  const maincategoryAPI = ( url = '/api/maincategory') => {
    return {
      fetchAll: () => axios.get(url),
      create: newRecord => axios.post(url, newRecord),
      update: (id, updateRecord) => axios.put(url, updateRecord),
      delete: id => axios.delete(url + id)
    }
  }

  //: API FUNCTIONS
  const subcategoryAPI = ( url ) => {
    return {
      fetchAll: () => axios.get(url),
      create: newRecord => axios.post(url, newRecord),
      update: (id, updateRecord) => axios.put(url, updateRecord),
      delete: id => axios.delete(url + id)
    }
  }

  const refreshPhotoList = () => {
    const url = '/api/maincategory/' + values.maincategoryId + '/photos/'
    if(values.subcategoryId != null){
      url = '/api/maincategory/' + values.maincategoryId + '/subcategory/' + values.subcategoryId + '/photos/'
    }
    console.log("refresh Photo URL: " + url)

    subcategoryAPI(url).fetchAll()
    .then(res => setPhotolist(res.data))
    .catch(err => console.log(err))

  }

  //: CREATE OR UPDATE
  const addOrEdit = (formData, onSuccess) => {
    if(formData.get('id')=="0")
      maincategoryAPI().create(formData)
        .then(res => {
          onSuccess();
          refreshPhotoList();
        })
        .catch(err => console.log(err))
    else
      maincategoryAPI().update(formData.get('id'), formData)
        .then(res => {
          onSuccess();
          refreshPhotoList();
        })
        .catch(err => console.log(err))
  }


  const resetForm = () =>{
    setValues(initialFieldValues)
    document.getElementById('image-uploader').value = null;
    setErrors({});
  }

  const showRecordDetails = data => {
    setRecordForEdit(data);
  }

  const onDelete = (e, id) => {
    e.stopPropagation();
    if(window.confirm('Are you sure you want to delete this record?'))
    maincategoryAPI().delete(id)
    .then(res => refreshMainCategoryList())
    .catch(err => console.log(err))
  }

  const handleInputChange = e => {
    const {name, value} = e.target;
    setValues({
      ...values,
      [name]: value
    })
  }

 //: REFRESH and GET ALL MAIN CATEGORIES
 function refreshMainCategoryList() {
  maincategoryAPI().fetchAll()
  .then(res => setMaincategorylist(res.data))
  .catch(err => console.log(err))
}

  const updateMainCategory = e => {
    subcategoryAPI('/api/maincategory/' + values.maincategoryId + "/subcategory/").fetchAll()
    .then(res => setSubcategorylist(res.data))
    .catch(err => console.log(err))
    refreshPhotoList();
    handleInputChange(e);
  }

  const updateSubcategory = e => {
    refreshPhotoList();
    handleInputChange(e);
  }


  const imageCard = data =>(
    <div className="card" onClick={() =>{showRecordDetails(data)}} >
      <div className="card-body">
        <img src={data.imageSrc} className="card-img-top rounded-circle"/>
        <div className="card-body">
          <h5>{data.name}</h5> <br/>
          <button className="btn btn-light delete-button" onClick={ e => onDelete(e, parseInt(data.Id))}>
          <BsFillTrashFill/>
          </button>
        </div>
      </div>
    </div>
  )
  return (
    <div className="container">
      {/* En metod för att lägga till MainCategory */}
        <div className="row">
          <div className="col-md-12 text-center">
              <h1 className="display-4">Photos</h1>
            </div>
        </div>
        <div className="col-md-4">
          <div className="container text-center">
            <p className="lead">Post Photo</p>
          </div>
          <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
            <div className="card">
              <img src={values.imageSrc}/>
              <div className="card-body">
                <div className="form-group">
                  <input type="file" accept="image/*" className={"form-control-file"+applyErrorClass('imageSrc')} 
                  onChange={showPreview} id="image-uploader"/>
                </div>

                {/* Main Cateogry */}
                <div className="form-group">
                  <select 
                  name="maincategoryId"
                  value={values.maincategoryId}
                  onChange={updateMainCategory}>
                    {maincategoryList.map(maincategory => (
                      <option value={maincategory.id}>{maincategory.name}</option>
                    )
                    )}
                  </select>
                </div>

                {/* Subcateogry */}
                <div className="form-group">
                  <select 
                  name="subcategoryId"
                  value={values.subcategoryId}
                  onChange={updateSubcategory}>
                    <option value={values.subcategoryId}> </option>
                    {subcategoryList.map(subcategory => (
                      <option value={subcategory.id}>{subcategory.name}</option>
                    )
                    )}
                  </select>
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
          <table>
            <tbody>
              {
                [...Array(Math.ceil(photoList.length/3))].map((e,i) =>
                  <tr key={i}>
                    <td>{photoList[3*i]?imageCard(photoList[3*i]): null}</td>
                    <td>{photoList[3*i+1]?imageCard(photoList[3*i+1]): null}</td>
                    <td>{photoList[3*i+2]?imageCard(photoList[3*i+2]): null}</td>
                  </tr>
                )
              }
            </tbody>
          </table>
        </div>

    </div>
  );
}


export default AdminPhotos;

