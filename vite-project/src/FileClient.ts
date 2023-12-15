import CancelToken from "./CancelToken";
import httpClient from "./DefaultHttpClient";

let _cancellationSource = new Object as CancelToken;
let _bucket = "";

const BASE_PATH = "/v1/FileApi";

class FileClient {
  constructor(b: string) {
    _bucket = b;
  }

  async listFiles() {
    return httpClient.get(`${BASE_PATH}/${_bucket}`)
      .then(response => response.data);
  }

  async deleteFile(id: string) {
    return httpClient.delete(`${BASE_PATH}/${_bucket}/${id}`)
      .then(response => response.data);
  }

  async createFile(name: string, type: string) {
    return httpClient.post(`${BASE_PATH}/${_bucket}`, {}, {
      headers: {
        "Plato-Filename": encodeURI(name),
        "Plato-Filetype": type
      }
    })
      .then(response => response.data);
  }

  cancelLoading() {
    _cancellationSource?.cancel();
  }

  async upload(id: string, file: File, onprogress: Function) {
    _cancellationSource = new CancelToken();

    return httpClient.put(`${BASE_PATH}/${_bucket}/${id}`, file, {
      headers: {
        "Content-type": "application/octet-stream"
      },
      cancelToken: _cancellationSource.token,
      onUploadProgress: e => onprogress(e)
    })
      .then(responce => responce.data)
      .catch(e => {
        if (!CancelToken.isCancel(e)) {
          throw e;
        }
      });
  }

  getBucket() {
    return _bucket;
  }
}

export default FileClient;
