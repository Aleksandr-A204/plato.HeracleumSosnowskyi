import axios, { type CancelTokenSource } from "axios";

import { ref } from "vue";

const source = ref(new Object as CancelTokenSource);

class CancelToken {
  constructor() {
    source.value = axios.CancelToken.source();
  }

  static isCancel(error: Error) {
    return axios.isCancel(error);
  }

  get token() {
    return source.value.token;
  }

  cancel() {
    source.value.cancel();
  }
}

export default CancelToken;
