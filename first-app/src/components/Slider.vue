<script>
import { VueperSlides, VueperSlide } from 'vueperslides'
import 'vueperslides/dist/vueperslides.css'


export default {
    name: 'SliDer',
    components: { VueperSlides, VueperSlide },
    data: () => ({
        slides: [
            {
                title: 'Slide #1',
                content: 'Slide 1 content.'
            },
            {
                title: 'Slide #2',
                content: `<div class="card" style="width: 18rem;">
  <img src="..." class="card-img-top" alt="...">
  <div class="card-body">
    <h5 class="card-title">Card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
</div>`
            }
        ]
    })

}
</script>

<template>
    <vueper-slides>
        <vueper-slide v-for="(slide, i) in slides" :key="i" :title="slide.title" :content="slide.content">
        </vueper-slide>
    </vueper-slides>
</template>