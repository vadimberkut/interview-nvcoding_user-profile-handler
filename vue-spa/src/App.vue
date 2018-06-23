<template>
  <div id="app">
    <div class="app-wrapper">
      <Menu />
      <Content />
    </div>
  </div>
</template>

<script>
import Menu from './components/Menu';
import Content from './components/Content';

import store from './store.js';

export default {
  name: 'App',
  components: {
    Menu,
    Content
  },
  created: function() {
    store.actions.getUserProfiles(store.state.userProfilesRequestParams).then((profiles) => {
      // Navigate to first profile
      if(profiles.length > 0) {
        let first = profiles[0];
        this.$router.push(`/user/edit/profile/${first.id}`);
      }
      else {
        // Navigate create page
        this.$router.push(`/user/create`);
      }
    });
    store.actions.getRoles();
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
}
</style>

<style lang="scss">
  html {
    font-size: 14px;
  }

  body {
    padding: 0;
    margin: 0;
    overflow: hidden;
  }

  body *, body *::after, body *::before {
    box-sizing: border-box;
  }

  a {
    color: inherit;
    text-decoration: none;
  }

  .app-wrapper {
    display: flex;

    
    .content {
      width: 75%;
      min-width: 350px;
      min-height: 100vh;
      background-color: #fbfbfb;
    }

    // General form styles
    input[type="text"], input[type="email"] {
      border: none;
      outline: none;
      background-color: transparent;
      border-bottom: 2px solid #8db0ff;
      width: 100%;
      padding: 0.5rem 0;

      &:focus, &:active {
          outline: none;
          border-color: #4881ff;
      }
    }

    .input-group {
        margin-bottom: 3rem;
        width: 400px;

        label {
            margin-bottom: 0;
            display: block;
            font-weight: bold;
            font-size: 1rem;
        }

        .input-control {
            width: 100%;
        }
    }

    .radio-group {
      display: flex;
      
      label {
        display: flex;
        flex-direction: column;
        align-items: center;
        cursor: pointer;

        &:not(:last-child) {
          margin-right: 2rem;
        }

        &::before {
          content: '';

          
        }

        input[type="radio"] {
          display: none;
        }
        input[type="radio"] {
          display: none;
        }

        .radio-check {
          display: block;
          position: relative;
          width: 16px;
          height: 16px;
          border: 2px solid #8db0ff;
          margin-bottom: 0.5rem;
          border-radius: 90%;
        }
        input[type="radio"]:checked ~ .radio-check {
          &:after {
            content: '';
            display: block;
            position: absolute;
            top: 1px;
            left: 1px;
            width: 10px;
            height: 10px;
            background-color: #8db0ff;
            border-radius: 90%;
          }
        }
      }
    }

    .toggle-group {
      .toggle-control {

        &:not(:last-child) {
          margin-right: 2rem;
        }

        label {
          display: flex;
          align-items: center;
          cursor: pointer;
        }

        input[type="checkbox"]  {
          display: none;
        }

        input[type="checkbox"] ~ .on-text, input[type="checkbox"] ~ .off-text {
          margin-right: 1rem;
          transition: display 1s linear;
        }

        input[type="checkbox"] ~ .on-text {
          display: none;
        }
        input[type="checkbox"] ~ .off-text {
          display: block;
        }
        input[type="checkbox"]:checked ~ .on-text {
          display: block;
        }
        input[type="checkbox"]:checked ~ .off-text {
          display: none;
        }

        input[type="checkbox"] ~ .toggle {
            margin-right: 1rem;
            width: 50px;
            height: 20px;
            background-color: #ff8dbd;;
            border-radius: 10px;
            position: relative;

          &::before {
            content: '';
            display: block;
            position: absolute;
            width: 16px;
            height: 16px;
            background-color: #fff;
            top: 2px;
            left: 2px;
            border-radius: 90%;
            transition: all 1s ease;
          }
        }
        input[type="checkbox"]:checked ~ .toggle {
          background-color: #8db0ff;

          &::before {
            right: 2px;
            left: initial;
          }
        }
      }
    }
  }
</style>

