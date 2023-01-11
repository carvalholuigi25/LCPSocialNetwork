export interface Animes {
    animeId: number | null;
    title: string | null;
    desc: string | null;
    mainImage: string | null;
    cover: string | null;
    gallery: GalleryAnimes[] | null;
    reviews: ReviewAnimes[] | null;
    authorsInfo: string | null;
    castInfo: string | null;
    company: string | null;
    publisher: string | null;
    distributor: string | null;
    certificationAge: number | null;
    totalDuration: number | null;
    dateStart: string | null;
    dateEnd: string | null;
    rating: number | null;
    isFeatured: boolean | null;
    isFavorite: boolean | null;
    dateCreated: string | null;
    usersId: number | null;
}

export interface GalleryAnimes {
    galleryAnimeId: number | null;
    title: string | null;
    date: string | null;
    type: string | null;
    src: string | null;
    animeId: number | null;
    userId: number | null;
}

export interface ReviewAnimes {
    reviewAnimeId: number | null;
    title: string | null;
    text: string | null;
    date: string | null;
    src: string | null;
    rating: number | null;
    animeId: number | null;
    userId: number | null;
}