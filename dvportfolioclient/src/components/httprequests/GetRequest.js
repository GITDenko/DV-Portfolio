import axios from "axios";
import { useEffect, useState } from "react";

const GetRequest = (url) => {
    const [data, setData] = useState(0);
    useEffect(() => {
      axios.get(url)
      .then(res => {
        setData(res.data);
      })
      .catch(err => {
        console.log(err)
      });
    }, [setData])
    return data;
  }

  export default GetRequest;