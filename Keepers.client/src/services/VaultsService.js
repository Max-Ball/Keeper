import { AppState } from "../AppState";
import { api } from "./AxiosService";

class VaultsService {
  async getVault(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.vaults.push(res.data)
  }

  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
    AppState.vaults = AppState.vaults.filter(v => v.id != id)
  }

  async getVaultsByAccount() {
    const res = await api.get('account/vaults')
    AppState.vaults = res.data
  }
}

export const vaultsService = new VaultsService();