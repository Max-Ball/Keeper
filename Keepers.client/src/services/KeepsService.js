import { AppState } from "../AppState"
import { api } from "./AxiosService"
import { masonryService } from "./MasonryService"

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }
  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
  }

  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
  }

  async createKeep(newKeep) {
    const res = await api.post('/api/keeps', newKeep)
    AppState.keeps.unshift(res.data)
  }

  async addKeepToVault(newKeepToVault) {
    const res = await api.post('api/vaultkeeps', newKeepToVault)
    AppState.vaultKeeps.push(res.data)
    console.log(res.data);
    console.log(AppState.vaultKeeps);
  }

  async removeKeepFromVault(id) {
    await api.delete(`/api/vaultkeeps/${id}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.vaultKeepId != id)
  }
}

export const keepsService = new KeepsService()