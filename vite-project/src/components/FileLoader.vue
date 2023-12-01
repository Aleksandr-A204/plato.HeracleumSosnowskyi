<template>
  <div class="block-fileloader">
    <label class="label">
      <slot />
    </label>
    <input
      class="input-file"
      type="file"
      accept="image/tiff"
      @change="event => readFile(event.target)"
    >
    <button
      @click="uploadFile"
      v-if="showMode">
      Загрузить
    </button>
  </div>
</template>

<script lang="ts">
import _ from "lodash";
import FileClient from "../API/FileClient";

import { AxiosError } from "axios";
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    filekey: {
      type: String,
      required: true
    }
  },

  data() {
    return {
      showMode: false,
      isLoading: false,
      fileClient: new FileClient(),
      responseData: {} as any,
      file: {} as File
    };
  },

  mounted() {
    //this.fileClient = new FileClient();
  },

  methods: {
    async readFile(eventTarget: any) {
      const files = eventTarget.files;
      if (!files) {
        return;
      }

      this.file = files[0];

      if (!_.includes(this.file.name, this.filekey)) {
        console.error(`Ошибка! Пожалуйста, ещё раз выберите файл с именем, содержащим ${this.filekey}.`);
        return;
      }

      try {
        this.responseData = await this.fileClient.createFile({
          fileName: this.file.name,
          MimeType: this.file.type,
          lastModified: this.file.lastModified
        });

        this.showMode = true;
      }
      catch (exception) {
        exception instanceof AxiosError ? console.error(exception.response?.data) : console.error(exception);
      }
      finally {
        this.isLoading = false;
      }
    },

    async uploadFile() {
      if (!this.responseData.fileId) {
        return console.error("Не удалось загрузить файл. Попробуйте ещё раз выберите файл, пожалуйста.");
      }

      try {
        await this.fileClient.uploadFile(this.responseData.fileId, this.file);
      }
      catch (error) {
        error instanceof AxiosError ? console.error(error.response?.data) : console.error(error);
      }
      finally {
        this.responseData = {},
        this.file = {} as File;

        this.showMode = false;
      }
    }
  }
});
</script>

<style scoped>
.block-fileloader{
  margin: 5px 0px;
}
.label{
margin: 10px;
}
</style>
