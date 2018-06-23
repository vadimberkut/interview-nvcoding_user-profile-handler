import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import UserEdit from '@/components/UserEdit'
import UserRoleEdit from '@/components/UserRoleEdit'
import UserSettingsEdit from '@/components/UserSettingsEdit'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/user/create',
      name: 'UserCreate',
      component: UserEdit
    },
    {
      path: '/user/edit/profile/:id',
      name: 'UserEdit',
      component: UserEdit,
      props: true
    },
    {
      path: '/user/edit/role',
      // name: 'UserRoleEdit',
      component: UserRoleEdit
    },
    {
      path: '/user/edit/settings',
      // name: 'UserSettingsEdit',
      component: UserSettingsEdit
    }
  ]
})
