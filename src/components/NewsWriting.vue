<script>
import axios from 'axios'
import '../assets/newsWriting.css'
const current = new Date()
const API_URL = import.meta.env.VITE_API_URL


export default {
  name: 'NewsPanel',
  data() {
    return {
      infos: [],
      newsTitle: '',
      newsContent: '',
      date: `${current.getFullYear()}-${current.getMonth()+1}-${current.getDate()}`,
      error: null
    }
  },
  methods: {
    async createNews() {
      try {
        const response = await axios.post(API_URL, {
          title: this.newsTitle,
          content: this.newsContent,
          date: new Date().toISOString().split('T')[0]
        })

        this.infos.unshift({
          ...response.data,
          isEditing: false,
          originalData: {...response.data}
        })

        this.newsTitle = ''
        this.newsContent = ''
      } catch (error) {
        this.error = 'Error while creating news'
      }
    },
    goToHome() {
      this.$router.push('/')
    },
  }
}
</script>

<template>
  <div class="container">
  <div class="news-creation">
    <h1 class="write-news-title">Write a news</h1>
    <form @submit.prevent="createNews" class="creation-form">
      <div class="form-group">
        <label>Title :</label>
        <input v-model="newsTitle" required class="form-input" value={this.newsTitle}/>
      </div>

      <div class="form-group">
        <label>Content :</label>
        <textarea v-model="newsContent" required class="form-textarea" value={this.newsContent}></textarea>
      </div>

      <button type="submit" class="btn submit">Publish</button>
    </form>
  </div>
    <div class="newspaper-cover">
      <div class="header">
        <h1 class="title">{{ this.newsTitle }}</h1>
        <p class="date">{{ this.date }}</p>
      </div>
      <div class="main-image">
        <!--<img :src="imageSrc" alt="Newspaper main image" /> !-->
      </div>
      <div class="summary">
        <p>{{ this.newsContent }}</p>
      </div>
    </div>
  </div>
  <button @click="goToHome" class="navigate-home-button">See the published news</button>
</template>

