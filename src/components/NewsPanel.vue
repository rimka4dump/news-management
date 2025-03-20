<script>
import axios from 'axios'
import NewsModal from './NewsModal.vue'
import '../assets/newsPanel.css'
const API_URL = import.meta.env.VITE_API_URL



export default {
  name: 'NewsPanel',
  components: {
    NewsModal
  },
  data() {
    return {
      infos: [],
      newsTitle: '',
      newsContent: '',
      error: null,
      isEditing: false,
      selectedArticle: null,
    }
  },
  mounted() {
    this.fetchNews()
  },
  methods: {
    async fetchNews() {
      try {
        const response = await axios.get(API_URL)
        this.infos = response.data.map(article => ({
          ...article,
          isEditing: false,
          originalData: { ...article }
        }))
      } catch (error) {
        this.error = 'Error while loading updates'
      }
    },

    async updateNews(article) {
      try {
        await axios.put(`${API_URL}${article.id}`, {
          id: article.id,
          title: article.title,
          content: article.content
        })
        article.isEditing = false
      } catch (error) {
        this.error = 'Update error'
      }
    },

    async deleteNews(id) {
      if (confirm('Confirm the delete ?')) {
        try {
          await axios.delete(`${API_URL}${id}`)
          this.infos = this.infos.filter(article => article.id !== id)
        } catch (error) {
          this.error = 'Error while deleting news'
        }
      }
    },

    cancelEdit(article) {
      article.title = article.originalData.title
      article.content = article.originalData.content
      article.isEditing = false
    },
    goToCreate() {
      this.$router.push('/create')
    },
    openEditModal(article) {
      this.selectedArticle = { ...article }
      this.isEditing = true
    },

    closeModal() {
      this.isEditing = false
      this.selectedArticle = null
      this.fetchNews()
    }
  }
}
</script>

<template class="news-panel">
  <div class="page-container">
    <div class="news-list">
      <span class="news-title">
        <h1>News</h1>
        <button @click="goToCreate" class="navigate-button">Write a news</button>
      </span>
      <div v-if="error" class="error-message">{{ error }}</div>

      <div class="table-container">
        <table>
          <thead>
          <tr>
            <th>Title</th>
            <th>Content</th>
            <th>Date</th>
            <th>Actions</th>
          </tr>
          </thead>

          <tbody>
          <tr v-for="article in infos" :key="article.id" class="article-row">
            <td>
              <input v-if="article.isEditing" v-model="article.title" class="edit-input">
              <span v-else>{{ article.title }}</span>
            </td>

            <td>
              <textarea v-if="article.isEditing" v-model="article.content" class="edit-textarea"></textarea>
              <span v-else class="span-table">{{ article.content }}</span>
            </td>

            <td>{{ article.date }}</td>

            <td class="actions-cell">
              <template v-if="article.isEditing">
                <button @click="updateNews(article)" class="btn save">Save</button>
                <button @click="cancelEdit(article)" class="btn cancel">Cancel</button>
              </template>
              <template v-else>
                <button @click="openEditModal(article)" class="btn edit">Edit</button>
                <button @click="deleteNews(article.id)" class="btn delete">Delete</button>
              </template>
            </td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
  <NewsModal v-if="isEditing" :article="selectedArticle" @close="closeModal" />
</template>
