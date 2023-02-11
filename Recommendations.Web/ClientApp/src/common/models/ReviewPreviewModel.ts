export interface ReviewPreviewModel {
  id: string
  authorName: string
  authorLikesCount: number
  reviewTitle: string
  productName: string
  creationDate: Date
  category: string
  averageRate: number
  tags: string[]
  imageUrl: string
}
