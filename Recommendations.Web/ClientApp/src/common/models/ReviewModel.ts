export interface ReviewModel {
  authorName: string
  averageRate: number
  likeCount: number
  reviewTitle: string
  productName: string
  category: string
  authorRate: number
  tags: string[]
  imageUrl: string
  creationDate: Date
  isLike: boolean
  userRating: number
}
