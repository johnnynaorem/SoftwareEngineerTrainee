<template>
  <div class="hello">
    <h2>Hi, {{ name }} -- {{ msg }}</h2>
    <h4 v-html=" someContent "></h4>
  </div>
  <div v-bind:id="divId1">This is the v-bind:id Tag</div>
  <div :id="divId2">This is the v-bind:id Tag</div>
  <div id="functionContent">{{ myConent() }}</div>
  <div v-if="myCondition">This is True</div>
  <div>
    <ul>
      <li v-for="item in datas" :key="item.id">{{ item.name }}</li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data(){
    return{
      name: "Johnny Naorem",
      someContent: "<p>This is returned from the Data Method<p>",
      divId1: "div1",
      divId2: "div2",
      myConent: () => {
        return "This is from Method Bind"
      },
      myStyle: "color: blue;",
      myCondition: false,

      datas: [
        {
          id: 1,
          name: "Johnny Naorem"
        },
        {
          id: 2,
          name: "Laishram Rohit"
        },
        {
          id: 3,
          name: "Niharika"
        },
        {
          id: 4,
          name: "Disha"
        }
      ]
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
h4 {
  margin: 0;
  color: red;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

#functionContent{
  font-size: 19px;
  color: blue;
  font-weight: bold;
}
</style>
