import React, { useState, useEffect } from "react";
import axios from "axios";
import "../index.css";
import { BsFillTrashFill } from "react-icons/bs";

const defaultImageSrc = '/img/missingimage.png'
const initialFieldValues = {
  id: 0,
  name: '', //Main Cateogry Name
  imageUrl: '', //imageName
  imageSrc: defaultImageSrc,
  imageFile: null //imageFile
}

const AdminMainCateogry = () => {
  const [values, setValues] = useState(initialFieldValues)
  const [errors, setErrors] = useState({})
  const [recordForEdit, setRecordForEdit] = useState(null) /// Pass recordforEdit to child och AddorEdit
  const [maincategoryList, setMaincategorylist] = useState([])


  //Use effect på maincategory, sub, photo, video etc.
  useEffect(() =>{
    if(recordForEdit != null)
      setValues(recordForEdit);
      refreshMainCategoryList();
    }, [recordForEdit])


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
    temp.name = values.name==""?false:true;
    temp.imageSrc = values.imageSrc==defaultImageSrc?false:true;
    setErrors(temp)
    return Object.values(temp).every(x => x==true)
  }

  const handleFormSubmit = e => {
    e.preventDefault()
    if(validate()){
      const formData = new FormData()
      formData.append('id', values.id)
      formData.append('name', values.name)
      //formData.append('hidden', values.hidden) // Behövs nog ej
      formData.append('imageUrl', values.imageUrl) //ImageName
      formData.append('imageFile', values.imageFile)
      addOrEdit(formData, resetForm)
    }
  }

  const applyErrorClass = field => ((field in errors && errors[field]==false)?' invalid-field': '')

  //: API FUNCTIONS
  const maincategoryAPI = ( url = '/api/maincategory/') => {
    return {
      fetchAll: () => axios.get(url),
      create: newRecord => axios.post(url, newRecord),
      update: (id, updateRecord) => axios.put(url, updateRecord),
      delete: id => axios.delete(url + id)
    }
  }

  //: REFRESH and GET ALL MAIN CATEGORIES
  function refreshMainCategoryList() {
    maincategoryAPI().fetchAll()
    .then(res => setMaincategorylist(res.data))
    .catch(err => console.log(err))
  }

  //: CREATE OR UPDATE
  const addOrEdit = (formData, onSuccess) => {
    if(formData.get('id')=="0")
      maincategoryAPI().create(formData)
        .then(res => {
          onSuccess();
          refreshMainCategoryList();
        })
        .catch(err => console.log(err))
    else
      maincategoryAPI().update(formData.get('id'), formData)
        .then(res => {
          onSuccess();
          refreshMainCategoryList();
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
    console.log(id, parseInt(id))
    e.stopPropagation();
    if(window.confirm('Are you sure you want to delete this record?'))
    maincategoryAPI().delete(id)
    .then(res => refreshMainCategoryList())
    .catch(err => console.log(err))
  }

  const imageCard = data =>(
    <div className="card" onClick={() =>{showRecordDetails(data)}} >
      <div className="card-body">
        <img src={data.imageSrc} className="card-img-top rounded-circle"/>
        <div className="card-body">
          <h5>{data.name}</h5> 
          <p> Id: {parseInt(data.id)}</p>
          <button className="btn btn-light delete-button" onClick={ e => onDelete(e, data.id)}>
          <BsFillTrashFill/>
          </button>
        </div>
      </div>
    </div>
  )
  return (
    <div className="container">
      {/* POST */}
        <div className="row">
          <div className="col-md-12 text-center">
              <h1 className="display-4">Main Categories</h1>
            </div>
        </div>
        <div className="col-md-4">
          <div className="container text-center">
            <p className="lead">Post Main Category</p>
          </div>
          <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
            <div className="card">
              <img src={values.imageSrc}/>
              <div className="card-body">
                <div className="form-group">
                  <input type="file" accept="image/*" className={"form-control-file"+applyErrorClass('imageSrc')} 
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
        
        {/* LIST */}
        <div className="col-md-8">
          <table>
            <tbody>
              {
                [...Array(Math.ceil(maincategoryList.length/3))].map((e,i) =>
                  <tr key={i}>
                    <td>{maincategoryList[3*i]?imageCard(maincategoryList[3*i]): null}</td>
                    <td>{maincategoryList[3*i+1]?imageCard(maincategoryList[3*i+1]): null}</td>
                    <td>{maincategoryList[3*i+2]?imageCard(maincategoryList[3*i+2]): null}</td>
                  </tr>
                )
              }
            </tbody>
          </table>
        </div>

    </div>
  );
}


export default AdminMainCateogry;

