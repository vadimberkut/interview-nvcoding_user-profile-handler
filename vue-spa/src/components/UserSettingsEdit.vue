<template>
    <div class="user-settings-editor">
        <div class="toggle-group">
            <div class="toggle-control">
                <label>
                    <input v-model="private.settings.enabled" v-on:change="onSettingsChange" type="checkbox" />
                    <div class="toggle"></div>
                    <div class="on-text">Disable</div>
                    <div class="off-text">Enable</div>
                </label>
            </div>
        </div>
    </div>
</template>

<script>
import _ from 'lodash';
import store from '../store';

export default {
    name: 'UserSettingsEdit',
    data: function() {
        return {
            shared: store.state,
            private: {
                settings: _.cloneDeep(store.state.editableUserProfile.settings)
            }
        }
    },
    created: function() {
        let self = this;

        this.updateSettings = _.debounce(function() {
            // let settings = { ...self.shared.editableUserProfile.settings, ...self.private.settings };
            let settings = self.private.settings;
            store.actions.updateSettings(settings).then(() => {
            }).catch(err => {
                // Revert vm to match the store
                self.private.settings = _.cloneDeep(store.state.editableUserProfile.settings)
            });
        }, 500);
    },
    methods: {
        onSettingsChange: function() {
            this.updateSettings();
        }
    }
}
</script>

<style lang="scss" scoped>
    .user-settings-editor {

    }
</style>
