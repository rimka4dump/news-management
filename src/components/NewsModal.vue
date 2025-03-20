<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Edit Article</h2>
      <label>Title:</label>
      <input v-model="editedArticle.title" class="edit-input" />
      <div>
      <label>Content:</label>
      <textarea v-model="editedArticle.content" class="edit-textarea"></textarea>
      </div>
      <div class="modal-actions">
        <button @click="saveChanges" class="btn save">Save</button>
        <button @click="$emit('close')" class="btn cancel">Cancel</button>
      </div>
    </div>
  </div>
  
</template>

<script>
import axios from 'axios'
import '../assets/newsModal.css'
const API_URL = import.meta.env.VITE_API_URL

export default {
  props: {
    article: Object
  },
  data() {
    return {
      editedArticle: { title: '', content: '', id: null },
      error: false,
    }
  },
  watch: {
    article: {
      immediate: true,
      handler(newArticle) {
        this.editedArticle = { ...newArticle }
      }
    }
  },
  methods: {
    async saveChanges() {
      this.error = null
      try {
        console.log(this.editedArticle)
        await axios.put(`${API_URL}${this.editedArticle.id}`, {
          id: this.editedArticle.id,
          title: this.editedArticle.title,
          content: this.editedArticle.content
        })
        this.$emit('close')
      } catch (error) {
        this.error = 'Update error'
      }
    }
  }
}
</script>

